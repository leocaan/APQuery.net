﻿// Guids.cs
// MUST match guids.h
using System;

namespace Symber.Web.APQuery.VSPackage
{
    static class GuidList
    {
        public const string guidPkgString = "560c3499-b182-4175-83b0-3809c72675a6";
        public const string guidCmdSetString = "d78cf03f-bf8f-44a8-8733-7b4bcc8fda10";

        public static readonly Guid guidCmdSet = new Guid(guidCmdSetString);
    };
}