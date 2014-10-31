using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Flower.Works
{
    internal class WorkObservables<TWork, TTriggeredWork> where TWork : IWorkBase where TTriggeredWork : ITriggeredWorkBase
    {
        private readonly TWork work;
        private event Action<TTriggeredWork> WorkTriggered;
        private event Action<TTriggeredWork> WorkExecuted;
        private event Action<Exception> TriggerErrored;
        private event Action TriggerCompleted;

        internal WorkObservables(TWork work)
        {
            this.work = work;
            Triggered = Observable.Create(CreateTriggeredSubscription());
            Executed = Observable.Create(CreateExecutedSubscription());
        }

        internal IObservable<TTriggeredWork> Triggered { get; private set; }
        internal IObservable<TTriggeredWork> Executed { get; private set; }

        internal void TriggeredWorkCreated(TTriggeredWork triggeredWork)
        {
            OnTriggeredWorkCreated(triggeredWork);
        }

        internal void TriggeredWorkExecuted(TTriggeredWork triggeredWork)
        {
            OnWorkExecuted(triggeredWork);
        }

        internal void OnTriggerErrored(Exception exception)
        {
            var handler = TriggerErrored;
            if (handler != null)
            {
                handler(exception);
            }
        }

        internal void OnWorkCompleted()
        {
            var handler = TriggerCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        private Func<IObserver<TTriggeredWork>, IDisposable> CreateTriggeredSubscription()
        {
            return CreateTriggeredSubscription;
        }

        private IDisposable CreateTriggeredSubscription(IObserver<TTriggeredWork> observer)
        {
            WorkTriggered += observer.OnNext;
            return Disposable.Create(() => { WorkTriggered -= observer.OnNext; });
        }

        private Func<IObserver<TTriggeredWork>, IDisposable> CreateExecutedSubscription()
        {
            return CreateExecutedSubscription;
        }

        private IDisposable CreateExecutedSubscription(IObserver<TTriggeredWork> observer)
        {
            if (work.State == WorkState.Completed ||
                work.State == WorkState.WorkerError ||
                work.State == WorkState.TriggerError)
            {
                observer.OnCompleted();
                return Disposable.Empty;
            }

            WorkExecuted += observer.OnNext;
            if (ShouldForwardErrorToSubscribers())
            {
                TriggerErrored += observer.OnError;
            }
            TriggerCompleted += observer.OnCompleted;

            return Disposable.Create(() =>
            {
                WorkExecuted -= observer.OnNext;
                TriggerCompleted -= observer.OnCompleted;
                TriggerErrored -= observer.OnError;
            });
        }

        private bool ShouldForwardErrorToSubscribers()
        {
            return work.Registration.WorkRegistry.Options.TriggerErrorBehavior ==
                   TriggerErrorBehavior.CompleteWorkAndForwardError;
        }

        private void OnTriggeredWorkCreated(TTriggeredWork triggeredWork)
        {
            var handler = WorkTriggered;
            if (handler != null)
            {
                handler(triggeredWork);
            }
        }

        private void OnWorkExecuted(TTriggeredWork triggeredWork)
        {
            var handler = WorkExecuted;
            if (handler != null)
            {
                handler(triggeredWork);
            }
        }
    }
}