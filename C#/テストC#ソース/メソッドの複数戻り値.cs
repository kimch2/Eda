
	private void button1_Click(object sender, EventArgs e)
    {
      var packed = GetHoge ();
      if (packed.IsOK)
      {
        textBox1.Text = packed.Name;
      }
    }

    private dynamic GetHoge ()
    {
	  // 無名関数で
      return new { Name = "ほげ", IsOK = true };
    }
    
