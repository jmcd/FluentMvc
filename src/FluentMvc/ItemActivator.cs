namespace FluentMvc
{
    using System;
    using Configuration;

    public abstract class ItemActivator
    {
        public Type Type { get; protected set; }

        public T Activate<T>(IFluentMvcObjectFactory fluentMvcObjectFactory)
        {
            return ActivateCore<T>(fluentMvcObjectFactory);
        }
        
        public object Activate(IFluentMvcObjectFactory fluentMvcObjectFactory)
        {
            return ActivateCore<object>(fluentMvcObjectFactory);
        }

        protected virtual T ActivateCore<T>(IFluentMvcObjectFactory fluentMvcObjectFactory)
        {
            return fluentMvcObjectFactory.Resolve<T>(Type);
        }
    }

    public class TypeItemActivator : ItemActivator
    {
        public TypeItemActivator(Type type)
        {
            Type = type;
        }
    }

    public class InstanceItemActivator : ItemActivator
    {
        public object Instance { get; set; }

        public InstanceItemActivator(object instance)
        {
            Instance = instance;
            Type = Instance.GetType();
        }

        protected override T ActivateCore<T>(IFluentMvcObjectFactory fluentMvcObjectFactory)
        {
            return (T)Instance;
        }
    }
}