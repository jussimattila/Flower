using System;
using Flower.Workers;

namespace Flower.Works
{
    public interface IWorkRegistration
    {
        IWorkRegistry WorkRegistry { get; }
        WorkRegistryOptions Options { get; }
    }

    public interface IWorkRegistration<out TInput> : IWorkRegistration
    {
        IObservable<TInput> Trigger { get; }
    }

    public interface IActionWorkRegistration : IWorkRegistration<object>
    {
        Func<IScope<IWorker>> CreateWorkerScope { get; }
    }

    public interface IActionWorkRegistration<TInput> : IWorkRegistration<TInput>
    {
        Func<IScope<IWorker<TInput>>> CreateWorkerScope { get; }
    }

    public interface IFuncWorkRegistration<TInput, out TOutput> : IWorkRegistration<TInput>
    {
        Func<IScope<IWorker<TInput, TOutput>>> CreateWorkerScope { get; }
    }
}