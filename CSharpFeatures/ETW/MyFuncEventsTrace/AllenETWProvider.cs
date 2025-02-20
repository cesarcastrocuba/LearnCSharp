//---------------------------------------------------------------------
// <autogenerated>
//
//     Generated by Message Compiler (mc.exe)
//
//     Copyright (c) Microsoft Corporation. All Rights Reserved.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//---------------------------------------------------------------------




namespace MyFuncEventsTrace
{
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;

    public static class AllenProvider
    {
        //
        // Provider Allen-Provider Event Count 2
        //

        internal static EventProviderVersionTwo m_provider = new EventProviderVersionTwo(new Guid("f1bcaf2b-6b78-4d67-809d-4cfc1832a9b6"));
        //
        // Task :  eventGUIDs
        //
        private static Guid FunctionTracingId = new Guid("671935b6-70a0-47d7-a115-b2e427994066");

        //
        // Event Descriptors
        //
        private static EventDescriptor FunctionEntry;
        private static EventDescriptor FuntionExit;


        static AllenProvider()
        {
            unchecked
            {
                FunctionEntry = new EventDescriptor(0x1, 0x0, 0x10, 0x4, 0xa, 0x1, (long)0x8000000000000001);
                FuntionExit = new EventDescriptor(0x2, 0x0, 0x10, 0x4, 0xb, 0x1, (long)0x8000000000000002);
            }
        }


        //
        // Event method for FunctionEntry
        //
        public static bool EventWriteFunctionEntry(string funcName, int paramCount)
        {

            if (!m_provider.IsEnabled())
            {
                return true;
            }

            return m_provider.Template_template_function_trace(ref FunctionEntry, funcName, paramCount);
        }

        //
        // Event method for FuntionExit
        //
        public static bool EventWriteFuntionExit(bool hasReturnValue)
        {

            if (!m_provider.IsEnabled())
            {
                return true;
            }

            return m_provider.Template_template_function_result(ref FuntionExit, hasReturnValue);
        }
    }

    internal class EventProviderVersionTwo : EventProvider
    {
         internal EventProviderVersionTwo(Guid id)
                : base(id)
         {}


        [StructLayout(LayoutKind.Explicit, Size = 16)]
        private struct EventData
        {
            [FieldOffset(0)]
            internal UInt64 DataPointer;
            [FieldOffset(8)]
            internal uint Size;
            [FieldOffset(12)]
            internal int Reserved;
        }



        internal unsafe bool Template_template_function_trace(
            ref EventDescriptor eventDescriptor,
            string funcName,
            int paramCount
            )
        {
            int argumentCount = 2;
            bool status = true;

            if (IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* userData = stackalloc byte[sizeof(EventData) * argumentCount];
                EventData* userDataPtr = (EventData*)userData;

                userDataPtr[0].Size = (uint)(funcName.Length + 1)*sizeof(char);

                userDataPtr[1].DataPointer = (UInt64)(&paramCount);
                userDataPtr[1].Size = (uint)(sizeof(int)  );

                fixed (char* a0 = funcName)
                {
                    userDataPtr[0].DataPointer = (ulong)a0;
                    status = WriteEvent(ref eventDescriptor, argumentCount, (IntPtr)(userData));
                }
            }

            return status;

        }



        internal unsafe bool Template_template_function_result(
            ref EventDescriptor eventDescriptor,
            bool hasReturnValue
            )
        {
            int argumentCount = 1;
            bool status = true;

            if (IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* userData = stackalloc byte[sizeof(EventData) * argumentCount];
                EventData* userDataPtr = (EventData*)userData;

                int hasReturnValueInt = hasReturnValue ? 1 : 0;
                userDataPtr[0].DataPointer = (UInt64)(&hasReturnValueInt);
                userDataPtr[0].Size = (uint)(sizeof(int));

                status = WriteEvent(ref eventDescriptor, argumentCount, (IntPtr)(userData));
            }

            return status;

        }

    }

}
