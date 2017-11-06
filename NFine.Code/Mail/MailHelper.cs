/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace NFine.Code
{
	public class MailHelper
	{
		/// <summary>
		/// 邮件服务器地址
		/// </summary>
		public string MailServer { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string MailUserName { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string MailPassword { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string MailName { get; set; }

		/// <summary>
		/// 同步发送邮件
		/// </summary>
		/// <param name="to">收件人邮箱地址</param>
		/// <param name="subject">主题</param>
		/// <param name="body">内容</param>
		/// <param name="encoding">编码</param>
		/// <param name="isBodyHtml">是否Html</param>
		/// <param name="enableSsl">是否SSL加密连接</param>
		/// <returns>是否成功</returns>
		public bool Send(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true, bool enableSsl = false)
		{
			try
			{
				MailMessage message = new MailMessage();
				// 接收人邮箱地址
				message.To.Add(new MailAddress(to));
				message.From = new MailAddress(MailUserName, MailName);
				message.BodyEncoding = Encoding.GetEncoding(encoding);
				message.Body = body;
				//GB2312
				message.SubjectEncoding = Encoding.GetEncoding(encoding);
				message.Subject = subject;
				message.IsBodyHtml = isBodyHtml;

				SmtpClient smtpclient = new SmtpClient(MailServer, 25);
				smtpclient.Credentials = new System.Net.NetworkCredential(MailUserName, MailPassword);
				//SSL连接
				smtpclient.EnableSsl = enableSsl;
				smtpclient.Send(message);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public bool SendMailUse(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true, bool enableSsl = false)
		{
			string host = "smtp.163.com";// 邮件服务器smtp.163.com表示网易邮箱服务器    

			SmtpClient client = new SmtpClient();
			client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
			client.Host = host;//邮件服务器
			client.UseDefaultCredentials = true;
			client.Credentials = new System.Net.NetworkCredential(MailUserName, MailPassword);//用户名、密码

			//////////////////////////////////////
			string strfrom = MailUserName;
			//string strto = "1097352786@qq.com";
			//string strcc = "2605625733@qq.com";//抄送

			System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
			msg.From = new MailAddress(MailUserName, MailName);
			msg.To.Add(new MailAddress(to));
			//msg.CC.Add(strcc); 抄送

			msg.Subject = subject;//邮件标题   
			msg.Body = body;//邮件内容   
			msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
			msg.IsBodyHtml = true;//是否是HTML邮件   
			msg.Priority = MailPriority.High;//邮件优先级   

			try
			{
				client.Send(msg);
				Console.WriteLine("发送成功");
			}
			catch (System.Net.Mail.SmtpException ex)
			{
				throw new Exception("发送邮件出错");
			}

			return true;
		}
		/// <summary>
		/// 异步发送邮件 独立线程
		/// </summary>
		/// <param name="to">邮件接收人</param>
		/// <param name="title">邮件标题</param>
		/// <param name="body">邮件内容</param>
		/// <param name="port">端口号</param>
		/// <returns></returns>
		public void SendByThread(string to, string title, string body, int port = 25)
		{
			new Thread(new ThreadStart(delegate ()
			{
				try
				{
					SmtpClient smtp = new SmtpClient();
					//邮箱的smtp地址
					smtp.Host = MailServer;
					//端口号
					smtp.Port = port;
					//构建发件人的身份凭据类
					smtp.Credentials = new NetworkCredential(MailUserName, MailPassword);
					//构建消息类
					MailMessage objMailMessage = new MailMessage();
					//设置优先级
					objMailMessage.Priority = MailPriority.High;
					//消息发送人
					objMailMessage.From = new MailAddress(MailUserName, MailName, System.Text.Encoding.UTF8);
					//收件人
					objMailMessage.To.Add(to);
					//标题
					objMailMessage.Subject = title.Trim();
					//标题字符编码
					objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
					//正文
					objMailMessage.Body = body.Trim();
					objMailMessage.IsBodyHtml = true;
					//内容字符编码
					objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
					//发送
					smtp.Send(objMailMessage);
				}
				catch (Exception ex)
				{
					throw;
				}

			})).Start();
		}
	}
}
