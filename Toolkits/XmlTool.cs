using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SodaCL.Toolkits {

	internal class XmlTool {
		private XmlNodeList _nodes;
		private XmlDocument doc;

		public XmlTool(string path) {
			try {
				doc = new XmlDocument();
				doc.Load(path);
				_nodes = doc.DocumentElement.ChildNodes;
			}
			catch (XmlException ex) {
				Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Error, null, ex);
				throw;
			}
		}

		public void CreateNode(string nodeName) {
			var root = doc.CreateElement(nodeName);
			doc.AppendChild(root);
		}

		public string GetNodeValue(string nodeName) {
			foreach (var node in _nodes) {
				if (node is XmlElement element) {
					if (element.Name == nodeName) {
						return element.InnerText;
					}
				}
			}
			return null;
		}

		public void SetNodeValue(string nodeName, string value) {
			foreach (XmlNode node in _nodes) {
				if (node.Name == nodeName) {
					node.InnerText = value;
					doc.Save(Launcher.LauncherInfo.SODACL_SETTINGS);
					return;
				}
			}
		}
	}
}