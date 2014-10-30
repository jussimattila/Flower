using System;
using System.Collections.Generic;
using Flower.Workers;
using Flower.Works;

namespace Flower
{
    public interface IWorkRegistry
    {
        WorkRegistryOptions Options { get; }

        IEnumerable<IWorkBase> Works { get; }   
    
        IWork Register<TInput>(
           IObservable<TInput> trigger,
           IWorkerResolver workerResolver);

        IWork<TInput> Register<TInput>(
           IObservable<TInput> trigger,
           IWorkerResolver<TInput> workerResolver);

        IWork<TInput, TOutput> Register<TInput, TOutput>(
            IObservable<TInput> trigger,
            IWorkerResolver<TInput, TOutput> workerResolver);
        
        void Unregister(IWorkBase work);
    }
}