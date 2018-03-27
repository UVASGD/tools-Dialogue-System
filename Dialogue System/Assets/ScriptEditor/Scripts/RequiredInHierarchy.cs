using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScriptEditor {
    [System.Serializable]
    public class RequiredInHierarchyAttribute : PropertyAttribute {
        public readonly Type requiredType;
        public RequiredInHierarchyAttribute(Type requiredType) {
            this.requiredType = requiredType;
        }
    }
}
