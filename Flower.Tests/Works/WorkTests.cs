﻿using System;
using System.Reactive.Subjects;
using FakeItEasy;
using Flower.Tests.TestDoubles;
using Flower.Works;
using Xunit;

namespace Flower.Tests.Works
{
    public partial class WorkTests
    {
        [Fact]
        public void CanRegisterWorkerWithoutInput()
        {
            // Arrange
            var workRegistry = new WorkRegistry();
            var trigger = A.Fake<IObservable<int>>();
            var worker = A.Fake<IWorker>();

            // Act
            var work = workRegistry.Register(trigger, worker);

            // Assert
            Assert.Equal(worker, work.Registration.CreateWorkerScope().Worker);
        }

        [Fact]
        public void WorkTriggered()
        {
            // Arrange
            var trigger = new Subject<int>();
            var registry = new WorkRegistry();
            var work = registry.Register(trigger, new TestWorker());
            ITriggeredActionWork triggeredWork = null;

            // Act
            work.Triggered.Subscribe(tw => triggeredWork = tw);
            trigger.OnNext(42);

            // Assert
            Assert.NotNull(triggeredWork);
        }
    }
}