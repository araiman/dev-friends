using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFriends.Models
{
	public class History
	{
		public int UserId { get; set; }
		public DateTime Date { get; set; }
		public Emotion Emotion { get; set; }
	}

	public enum Emotion
	{
		SuperHappy,
		Happy,
		Neutral,
		Angry,
		Sad,
		Painful,
		Cold
	}
}