using System;
using System.Collections.Generic;
using VPNetExamples.Common;
using VpNet.Core;
using VpNet.Core.EventData;
using VpNet.Core.Structs;

namespace VPNetExamples.UnitTestsBot
{
    internal class UnitTestsBot : BaseExampleBot
    {
        private VpObject _model = new VpObject() { Model = "sign1.rwx" };

        public UnitTestsBot() { }

        public UnitTestsBot(Instance instance) : base(instance)
        {
            Instance.EventObjectCreateCallback += EventObjectCreateCallback;
            Instance.EventObjectChangeCallback += EventObjectChangeCallback;
            Instance.EventObjectDeleteCallback += EventObjectDeleteCallback;
        }

        void EventObjectCreateCallback(Instance sender, ObjectCreateCallbackArgs args)
        {
            if (args.VpObject == _model)
            {
                Console.WriteLine("created object number: {0}", _model.Id);
                Console.WriteLine("press enter to change object");
                Console.ReadLine();
                _model.Description = "Changed object";
                _model.Action = "Create Sign";
                Instance.ChangeObject(_model);
            }
        }

        void EventObjectChangeCallback(Instance sender, ObjectChangeCallbackArgs args)
        {
            if (args.VpObject == _model)
            {
                Console.WriteLine("changed object number: {0}", _model.Id);
                Console.WriteLine("press enter to delete object");
                Console.ReadLine();
                Instance.DeleteObject(_model);
            }
        }

        void EventObjectDeleteCallback(Instance sender, ObjectDeleteCallbackArgs args)
        {
            if (args.VpObject == _model)
            {
                Console.WriteLine("deleted object number: {0}", _model.Id);
            }
        }

        public override void Initialize()
        {
        
        }

        void EventObjectCreate(Instance sender, int sessionId, VpObject objectData)
        {
        }

        public override void  Disconnect()
        {
        }

        public void UnitTestA()
        {
            Instance.AddObject(_model);
        }
    }
}
