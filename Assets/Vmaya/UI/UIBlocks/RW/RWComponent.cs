

namespace Vmaya.UI.UIBlocks.RW
{
    public class RWComponent : UIBComponent
    {
        internal RWComponent Origin;

        public void setData(ComponentData data)
        {
            setJsonData(data.jsonData);
        }

        protected virtual string getPrefabName()
        {
            return Origin ? Origin.name : null;
        }

        public ComponentData getData()
        {
            ComponentData data = new ComponentData();
            data.prefabName = getPrefabName();
            data.jsonData = getJsonData();
            return data;
        }

        protected virtual string getJsonData()
        {
            return null;
        }

        protected virtual void setJsonData(string jsonData)
        {
        }

        public void Close()
        {
            Destroy(gameObject);

            UIBManager m = Manager;
            Vmaya.Utils.setTimeout(Manager, () => { m.onManualChangeBoxes.Invoke(); }, Consts.LRG_TIMEOUT);
        }
    }
}
