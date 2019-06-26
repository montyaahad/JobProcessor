using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobProcessor
{
    public class SingleJobSequence
    {
        private string _name;

        private string _dependsOn;

        public SingleJobSequence(string input)
        {
            var splitted = input.Trim(' ').Split(new string[] { "=>" }, StringSplitOptions.None);
            this._name = splitted[0];
            this._dependsOn = splitted[1];
        }

        public string GetJobName()
        {
            return this._name;
        }

        public string GetDependsOnName()
        {
            return this._dependsOn;
        }
    }
}
