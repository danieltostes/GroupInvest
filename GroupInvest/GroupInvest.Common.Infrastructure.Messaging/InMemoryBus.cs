using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Commands;
using GroupInvest.Common.Application.Events;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Common.Infrastructure.Messaging
{
    public abstract class InMemoryBus : IMediatorHandler
    {
        #region Propriedades
        protected Dictionary<Type, Type> commandHandlerList;
        protected Dictionary<Type, Type> eventHandlerList;
        protected Dictionary<string, List<Type>> subscribers;

        protected Dictionary<Type, Type> dependencyInjectionList;
        protected Dictionary<string, Type> dbContextInjectionList;
        protected Dictionary<string, string> dbContextConnectionString;
        protected Dictionary<Type, object> dependencyInjectionObjectList;

        protected Dictionary<Type, DbContext> dbContextInstances;
        #endregion

        #region Construtor
        public InMemoryBus()
        {
            commandHandlerList = new Dictionary<Type, Type>();
            eventHandlerList = new Dictionary<Type, Type>();
            subscribers = new Dictionary<string, List<Type>>();

            dependencyInjectionList = new Dictionary<Type, Type>();
            dependencyInjectionObjectList = new Dictionary<Type, object>();
            dbContextInjectionList = new Dictionary<string, Type>();
            dbContextConnectionString = new Dictionary<string, string>();

            dbContextInstances = new Dictionary<Type, DbContext>();

            ConfigureHandlers();
        }
        #endregion

        #region Métodos
        protected virtual void ConfigureHandlers()
        {
        }

        protected void SetCommandHandler<TCommand, THandler>()
            where TCommand : Command
            where THandler : CommandHandler
        {
            commandHandlerList.Add(typeof(TCommand), typeof(THandler));
        }

        protected void SetEventHandler<TEvent, THandler>()
            where TEvent : Event
            where THandler : GroupInvest.Common.Application.EventHandlers.EventHandler
        {
            eventHandlerList.Add(typeof(TEvent), typeof(THandler));
        }

        protected void SetSubscriber<T>(string evento)
        {
            if (subscribers.ContainsKey(evento))
                subscribers[evento].Add(typeof(T));
            else
                subscribers.Add(evento, new List<Type>() { typeof(T) });
        }

        protected void AddDependencyInjection<TInterface, TImplementation>()
        {
            dependencyInjectionList.Add(typeof(TInterface), typeof(TImplementation));
        }

        protected void AddDependencyInjection<TInterface>(object instance)
        {
            dependencyInjectionObjectList.Add(typeof(TInterface), instance);
        }

        protected void AddDbContext<T>(string nameSpace, string connectionString) where T : DbContext
        {
            dbContextInjectionList.Add(nameSpace, typeof(T));
            dbContextConnectionString.Add(nameSpace, connectionString);
        }

        public DbContext GetDbContext(string nameSpace)
        {
            if (dbContextInjectionList.Any(db => nameSpace.Contains(db.Key)))
            {
                var item = dbContextInjectionList.FirstOrDefault(db => nameSpace.Contains(db.Key));
                if (dbContextInstances.ContainsKey(item.Value))
                    return dbContextInstances[item.Value];
                else
                {
                    DbContext dbContext = Activator.CreateInstance(item.Value, new object[] { dbContextConnectionString[item.Key] }) as DbContext;
                    dbContextInstances.Add(item.Value, dbContext);

                    dbContext.Database.EnsureCreated();

                    return dbContext;
                }
            }
            return null;
        }

        private object ResolveDependecyInjection(Type type)
        {
            object obj = null;

            var constructors = type.GetConstructors();
            if (constructors != null && constructors.Length > 0)
            {
                List<object> args = new List<object>();

                var parameters = constructors[0].GetParameters();
                if (parameters.Length > 0)
                {
                    foreach (var param in parameters)
                    {
                        if (typeof(IMediatorHandler).IsAssignableFrom(param.ParameterType) && !typeof(IMediatorHandlerQueue).IsAssignableFrom(param.ParameterType))
                        {
                            args.Add(this);
                        }
                        else
                        {
                            if (dependencyInjectionObjectList.ContainsKey(param.ParameterType))
                            {
                                args.Add(dependencyInjectionObjectList[param.ParameterType]);
                            }
                            else if (param.ParameterType == typeof(DbContext))
                            {
                                if (dbContextInjectionList.Any(db => type.FullName.Contains(db.Key)))
                                {
                                    var item = dbContextInjectionList.First(db => type.FullName.Contains(db.Key));

                                    // verifica se já existe um DbContext instanciado
                                    if (dbContextInstances.ContainsKey(item.Value))
                                        args.Add(dbContextInstances[item.Value]);
                                    else
                                    {
                                        var dbContextInstance = Activator.CreateInstance(item.Value, new object[] { dbContextConnectionString[item.Key] });
                                        args.Add(dbContextInstance);

                                        dbContextInstances.Add(item.Value, (DbContext)dbContextInstance);
                                    }
                                }
                                else throw new Exception(string.Format("Injeção de dependência de DbContext para {0} não foi configurada", type.FullName));
                            }
                            else
                            {
                                if (!dependencyInjectionList.ContainsKey(param.ParameterType))
                                    throw new Exception(string.Format("Injeção de dependência para {0} não foi configurada", param.ParameterType.FullName));

                                var injectedObject = ResolveDependecyInjection(dependencyInjectionList[param.ParameterType]);
                                if (injectedObject == null)
                                    throw new Exception(string.Format("Não foi possível resolver a injeção de dependência para a interface {0}", param.ParameterType.FullName));

                                args.Add(injectedObject);
                            }
                        }
                    }
                    obj = Activator.CreateInstance(type, args.ToArray());
                }
                else obj = Activator.CreateInstance(type, args.ToArray());
            }

            return obj;
        }

        private OperationResult InvokeHandle(Type handlerType, object message)
        {
            var constructors = handlerType.GetConstructors();

            List<object> args = new List<object>();
            if (constructors != null && constructors.Length > 0)
            {
                var parameters = constructors[0].GetParameters();
                if (parameters.Length > 0)
                {
                    foreach (var param in parameters)
                    {
                        // Injeta o próprio ImMemoryBus como dependencia
                        if (typeof(IMediatorHandler).IsAssignableFrom(param.ParameterType) && !typeof(IMediatorHandlerQueue).IsAssignableFrom(param.ParameterType))
                        {
                            args.Add(this);
                        }
                        else if (param.ParameterType == typeof(DbContext))
                        {
                            if (dbContextInjectionList.Any(db => handlerType.FullName.Contains(db.Key)))
                            {
                                var item = dbContextInjectionList.First(db => handlerType.FullName.Contains(db.Key));

                                // verifica se já existe um DbContext instanciado
                                if (dbContextInstances.ContainsKey(item.Value))
                                    args.Add(dbContextInstances[item.Value]);
                                else
                                {
                                    var dbContextInstance = Activator.CreateInstance(item.Value, new object[] { dbContextConnectionString[item.Key] });
                                    args.Add(dbContextInstance);

                                    dbContextInstances.Add(item.Value, (DbContext)dbContextInstance);
                                }
                            }
                            else throw new Exception(string.Format("Injeção de dependência de DbContext para {0} não foi configurada", handlerType.FullName));
                        }
                        else
                        {
                            if (dependencyInjectionObjectList.ContainsKey(param.ParameterType))
                            {
                                args.Add(dependencyInjectionObjectList[param.ParameterType]);
                            }
                            else
                            {
                                if (!dependencyInjectionList.ContainsKey(param.ParameterType))
                                    throw new Exception(string.Format("Injeção de dependência para {0} não foi configurada", param.ParameterType.FullName));

                                var injectedObject = ResolveDependecyInjection(dependencyInjectionList[param.ParameterType]);
                                if (injectedObject == null)
                                    throw new Exception(string.Format("Não foi possível resolver a injeção de dependência para a interface {0}", param.ParameterType.FullName));

                                args.Add(injectedObject);
                            }
                        }
                    }
                }
            }

            var handler = Activator.CreateInstance(handlerType, args.ToArray());
            MethodInfo handle = handler.GetType().GetMethod("Handle", new Type[] { message.GetType() });

            if (handle != null)
                return handle.Invoke(handler, new object[] { message }) as OperationResult;
            else
                return new OperationResult(StatusCodeEnum.Error, string.Format("Método Handle não implementado com a assinatura necessária para o comando {0}", message.GetType().FullName));
        }
        #endregion

        #region IMediatorHandler
        public OperationResult PublishEvent<T>(T evento) where T : Event
        {
            var result = OperationResult.OK;

            if (eventHandlerList.ContainsKey(typeof(T)))
            {
                Type handlerType = eventHandlerList[typeof(T)];
                result = InvokeHandle(handlerType, evento);

                if (result.StatusCode != StatusCodeEnum.OK)
                    return result;
            }

            // notify subscribers
            var topic = typeof(T).Name;
            if (subscribers.ContainsKey(topic))
            {
                foreach (var sub in subscribers[topic])
                {
                    var types = sub.Assembly.GetTypes();
                    if (types != null && types.Length > 0)
                    {
                        var type = types.FirstOrDefault(tp => tp.Name.Contains(topic));
                        if (type != null)
                        {
                            var serializedEvent = JsonConvert.SerializeObject(evento);
                            var message = JsonConvert.DeserializeObject(serializedEvent, type);
                            result = InvokeHandle(sub, message);

                            if (result.StatusCode != StatusCodeEnum.OK)
                                return result;
                        }
                    }
                }
            }

            return result;
        }

        public OperationResult SendCommand<T>(T command) where T : Command
        {
            if (commandHandlerList.ContainsKey(typeof(T)))
            {
                Type handlerType = commandHandlerList[typeof(T)];
                return InvokeHandle(handlerType, command);
            }
            return new OperationResult(StatusCodeEnum.Error, string.Format("CommandHandler para o comando {0} não foi configurado.", typeof(T).FullName));
        }
        #endregion
    }
}
