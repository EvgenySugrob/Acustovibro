using UnityEngine;

namespace Vmaya.RW
{
    public class RWAny : RWEvents
    {
        [SerializeField]
        private Component component;
        private IRW _component;

        private void OnValidate()
        {
            if (component != null)
            {
                _component = findOtherRW(component);
                if (_component == null)
                {
                    Debug.Log("The component must implement the IRW interface");
                    component = null;
                }
                else component = _component as Component;
            }
            else _component = null;
        }

        virtual protected void Awake()
        {            
            if (component == null)
            {
                _component = GetComponentInChildren<IRW>();
                if (_component != null) component = (_component = findOtherRW(this)) as Component;
            } else OnValidate();
        }

        private IRW findOtherRW(Component parent)
        {
            IRW[] list = parent.GetComponentsInChildren<IRW>();
            foreach (IRW item in list)
                if ((object)this != item) return item;

            return null;
        }

        protected override void doReadData(dataRecord rec)
        {
            if (_component != null)
            {
                if (Provider) initProvider();
                _component.readRecord(rec);
            }
        }

        public override Indent getIndent()
        {
            return (_component != null) ? _component.getIndent() : base.getIndent();
        }

        protected override string doWriteData()
        {
            return (_component != null) ? _component.writeData() : "";
        }
    }
}