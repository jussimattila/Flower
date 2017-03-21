﻿using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid("97219a9e-6baa-4cad-9abc-65a56b482bf4")]
[assembly: AssemblyTitle("Flower")]
[assembly: AssemblyDescription(
    "Flower is a library for registering workers " +
    "that are submitted for execution on a work " +
    "runner when triggered by an observable.")]
[assembly: AssemblyProduct("Flower")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyCompany("None")]

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyVersion("0.0.0.1")]
[assembly: AssemblyFileVersion("0.0.0.1")]
[assembly: AssemblyInformationalVersion("0.0.0.1")]
[assembly: ComVisible(false)]
