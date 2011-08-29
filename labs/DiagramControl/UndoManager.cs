using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DiagramNet
{
	internal class UndoManager
	{
		protected MemoryStream[] List;
		protected int CurrPos = -1;
		protected int LastPos = -1;
		protected int Capacity;

	    public UndoManager(int capacity)
		{
		    Enabled = true;
		    List = new MemoryStream[capacity];
			Capacity = capacity;
		}

		public bool CanUndo
		{
			get
			{
				return (CurrPos != -1);
			}
		}

		public bool CanRedo
		{
			get
			{
				return (CurrPos != LastPos);
			}
		}

	    public bool Enabled { get; set; }

	    public void AddUndo(object o)
		{
			if (!Enabled) return;

			CurrPos++;
			if (CurrPos >= Capacity)
				CurrPos--;

			ClearList(CurrPos);

			PushList();

			List[CurrPos] = SerializeObject(o);
			LastPos = CurrPos;
		}

		public object Undo()
		{
			if (!CanUndo)
				throw new ApplicationException("Can't Undo.");

			var ret = DeserializeObject(List[CurrPos]);
			
			CurrPos--;
		
			return ret;
		}

		public object Redo()
		{
			if (!CanRedo)
				throw new ApplicationException("Can't Undo.");

			CurrPos++;

			return DeserializeObject(List[CurrPos]);
		}

		private MemoryStream SerializeObject(object o)
		{
			IFormatter formatter = new BinaryFormatter();
			var mem = new MemoryStream();
			formatter.Serialize(mem, o);
			mem.Position = 0;
			return mem;
		}

		private object DeserializeObject(MemoryStream mem)
		{
			mem.Position = 0;
			IFormatter formatter = new BinaryFormatter();
			return formatter.Deserialize(mem);
		}

	    private void ClearList(int p = 0)
		{
			if (CurrPos >= Capacity - 1)
				return;

			for(var i = p; i < Capacity; i++)
			{
				if (List[i] != null) List[i].Close();
				List[i] = null;
			}
		}

		private void PushList()
		{
		    if ((CurrPos < Capacity - 1) || (List[CurrPos] == null)) return;
		    List[0].Close();
		    for (var i = 1; i <= CurrPos; i++)
		    {
		        List[i - 1] = List[i];
		    }
		}
	}
}
