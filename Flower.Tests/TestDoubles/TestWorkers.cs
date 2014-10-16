﻿using System;
using System.Globalization;

namespace Flower.Tests.TestDoubles
{
    internal static class TestWorkers
    {
        internal static readonly TestWorkerIntSquared IntSquaredWorker = new TestWorkerIntSquared();
        internal static readonly TestWorkerInt2String Int2StringWorker = new TestWorkerInt2String();
        internal static readonly TestWorkerString2Int String2IntWorker = new TestWorkerString2Int();
    }

    internal class TestWorkerIntSquared : IWorker<int, int>
    {
        public static readonly Func<int, int> WorkerFunc = i => i * i;

        public int Execute(int input)
        {
            return WorkerFunc(input);
        }
    }

    internal class TestWorkerInt2String : IWorker<int, string>
    {
        public string Execute(int input)
        {
            return input.ToString(CultureInfo.InvariantCulture);
        }
    }

    internal class TestWorkerString2Int : IWorker<string, int>
    {
        public int Execute(string input)
        {
            return int.Parse(input);
        }
    }
}
