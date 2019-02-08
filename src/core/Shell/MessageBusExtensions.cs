﻿using Fuxion.Shell;
using Fuxion.Shell.Messages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using Fuxion.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Telerik.Windows.Controls;

namespace Fuxion.Shell
{
	public static class MessageBusExtensions
	{
		const string LAYOUT_FILE_PATH = "layout.xml";
		public static void LoadLayout(this IMessageBus me, string layoutFilePath = LAYOUT_FILE_PATH)
		{
			if(File.Exists(layoutFilePath))
				using(var str = File.OpenRead(layoutFilePath))
				{
					LoadLayout(me, str);
				}
		}
		public static void LoadLayout(this IMessageBus me, Stream layoutFileStream) => me.SendMessage(new LoadLayoutMessage(layoutFileStream));
		public static void SaveLayout(this IMessageBus me, string layoutFilePath = LAYOUT_FILE_PATH)
		{
			if (File.Exists(layoutFilePath)) File.Delete(layoutFilePath);
			using (var mem = new MemoryStream())
			{
				SaveLayout(me, mem);
				mem.Position = 0;
				File.AppendAllText(layoutFilePath, XDocument.Load(mem).ToString());
			}
		}
		public static void SaveLayout(this IMessageBus me, Stream layoutFileStream) => me.SendMessage(new SaveLayoutMessage(layoutFileStream));

		public static void OpenPanel(this IMessageBus me, PanelName name, params (string Key, object Value)[] args) => OpenPanel(me, name, args.Transform(list => new Dictionary<string, object>(list.Select(l => new KeyValuePair<string, object>(l.Key, l.Value)))));
		public static void OpenPanel(this IMessageBus me, PanelName name, Dictionary<string, object> args)
		{
			me.SendMessage(new OpenPanelMessage(name,args));
		}
		public static void ClosePanel(this IMessageBus me, PanelName name)
		{
			me.SendMessage(new ClosePanelMessage(name));
		}
		internal static void ClosePanel(this IMessageBus me, RadPane pane)
		{
			me.SendMessage(new ClosePanelMessage(pane));
		}
		public static void CloseAllPanelsWithKey(this IMessageBus me, string key)
		{
			me.SendMessage(new CloseAllPanelsWithKeyMessage(key));
		}

		public static void Lock(this IMessageBus me)
		{
			me.SendMessage(new LockMessage());
		}
		public static void Unlock(this IMessageBus me)
		{
			me.SendMessage(new UnlockMessage());
		}
	}
}