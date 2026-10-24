class Widget {
  /// @CSharpLua.Get = "Widget.Default(getmetatable({this}))"
  public extern int Default { get; }

  /// @CSharpLua.Set = "{this}.depth = {0}"
  public extern int Depth { set; }

  /// @CSharpLua.Get = "MAGIC"
  public static extern int Magic { get; }

  public int Bare() => Default;

  public int BareStatic() => Magic;

  public static Widget Make() => new Widget { Depth = 7 };

  public int Qualified() => this.Default;

  public void Put() {
    Depth = 3;
    this.Depth = 4;
  }
}

class Gadget : Widget {
  public int Inherited() => Default;

  public int FromBase() => base.Default;

  public void PutInherited() {
    Depth = 5;
    base.Depth = 6;
  }
}
