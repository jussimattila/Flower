﻿namespace Flower.Tests.Triggers
{
    using System;
    using System.Collections.Generic;
    using Flower.Tests.TestDoubles;
    using Flower.Triggers;
    using Xunit;

    public class TriggerTests
    {
        [Fact]
        public void TriggersTriggerWork()
        {
            // Arrange
            var workRegistry = new WorkRegistry();
            ITrigger<int> trigger = new TriggerBase<int>();
            var work1 = workRegistry.RegisterWorker(trigger, new TestWorkerIntToString());
            var work2 = work1.Pipe(new TestWorkerStringToInt());
            var work3 = work2.Pipe(new TestWorkerIntToIntSquared());
            var results = new List<int>();
            work3.Output.Subscribe(i => results.Add(i));

            // Act
            trigger.Trigger(3);
            trigger.Trigger(5);

            // Assert
            Assert.Equal(new []{3*3, 5*5}, results);
            Assert.Equal(WorkState.Active, work1.State);
            Assert.Equal(WorkState.Active, work2.State);
            Assert.Equal(WorkState.Active, work3.State);
        }
    }
}
