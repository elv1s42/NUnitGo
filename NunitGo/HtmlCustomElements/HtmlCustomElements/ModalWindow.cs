using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.HtmlCustomElements.CSSElements;
using NunitGo.Utils;

namespace NunitGo.HtmlCustomElements.HtmlCustomElements
{
	public class ModalWindow : HtmlBaseElement
	{
		public string BackgroundId;
		private static int _zIndex = 1002;
		private static int _zIndexBcg;
		private static string _width;
		private static string _left;
		private static string _idBackground;
		public string InnerHtml;
		public string ModalWindowHtml;
		public static string StyleString
		{
			get { return GetStyle(); }
		}
		
		public ModalWindow(string id, string innerHtml, int zIndex = 1004, int width = 80, int zIndexBcg = 0)
		{
			Id = id;
			InnerHtml = innerHtml;
			_zIndexBcg = (zIndexBcg == 0) ? zIndex - 1 : zIndexBcg;
			_zIndex = zIndex;
			_width = width.ToString("D") + @"%";
			_left = (100 - width).ToString("D") + @"%";
			_idBackground = Ids.GetBackgroundId(id);
			BackgroundId = _idBackground;
			Style = GetStyle();
			ModalWindowHtml = GetWindow();
		}

		private static string GetStyle()
		{
			var modalWindowCssSet = new CssSet("modal-window-style");
			modalWindowCssSet.AddElement(new CssElement(".modal-window")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("box-sizing", "border-box"),
					new StyleAttribute(HtmlTextWriterStyle.Overflow, "auto"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
					new StyleAttribute(HtmlTextWriterStyle.Top, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px"),
					new StyleAttribute("border", "10px solid " + Colors.ModalBorderColor),
					new StyleAttribute(HtmlTextWriterStyle.Position, "fixed"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
			});
			return modalWindowCssSet.ToString();
		}

		private string GetWindow()
		{
			var stringWriter = new StringWriter();
			using (var writer = new HtmlTextWriter(stringWriter))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				var backgroundId = _idBackground;
				var background = new ModalBackground(backgroundId, _zIndexBcg.ToString("D"));
				writer.Write(background.ModalBackgroundHtml);
				writer.RenderEndTag();

				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, _left);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, _width);
				writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, _zIndex.ToString("D"));
				writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-window");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id + "-inner");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				var closeButton = new CloseButton(Id, backgroundId);
				writer.Write(closeButton.ButtonHtml);
                writer.RenderEndTag();
                
                writer.Write(InnerHtml);
                writer.RenderEndTag();
			}
			return stringWriter.ToString();
		}
	}
}
