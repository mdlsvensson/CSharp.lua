/*
Copyright 2017 YANG Huan (sy.yanghuan@gmail.com).

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Globalization;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpLua.LuaAst {
  public abstract class LuaLiteralExpressionSyntax : LuaExpressionSyntax {
    public abstract string Text { get; }
  }

  public sealed class LuaIdentifierLiteralExpressionSyntax : LuaLiteralExpressionSyntax {
    public LuaIdentifierNameSyntax Identifier { get; }

    public LuaIdentifierLiteralExpressionSyntax(string text) : this((LuaIdentifierNameSyntax)text) {
    }

    public LuaIdentifierLiteralExpressionSyntax(LuaIdentifierNameSyntax identifier) {
      Identifier = identifier;
    }

    public override string Text {
      get {
        return Identifier.ValueText;
      }
    }

    internal override void Render(LuaRenderer renderer) {
      renderer.Render(this);
    }

    public static readonly LuaIdentifierLiteralExpressionSyntax Nil = new(LuaIdentifierNameSyntax.Nil);
    // TODO: implicit operator for booleans
    public static readonly LuaIdentifierLiteralExpressionSyntax True = new(LuaIdentifierNameSyntax.True);
    public static readonly LuaIdentifierLiteralExpressionSyntax False = new(LuaIdentifierNameSyntax.False);
  }

  public sealed class LuaStringLiteralExpressionSyntax : LuaLiteralExpressionSyntax {
    public string OpenParenToken => Tokens.Quote;
    public LuaIdentifierNameSyntax Identifier { get; }
    public string CloseParenToken => Tokens.Quote;

    public LuaStringLiteralExpressionSyntax(LuaIdentifierNameSyntax identifier) {
      Identifier = identifier;
    }

    public override string Text {
      get {
        return Identifier.ValueText;
      }
    }

    internal override void Render(LuaRenderer renderer) {
      renderer.Render(this);
    }

    public static readonly LuaStringLiteralExpressionSyntax Empty = new(LuaIdentifierNameSyntax.Empty);
  }

  public sealed class LuaVerbatimStringLiteralExpressionSyntax : LuaLiteralExpressionSyntax {
    public override string Text { get; }
    public int EqualsCount { get; }
    public string OpenBracket => Tokens.OpenBracket;
    public string CloseBracket => Tokens.CloseBracket;

    public LuaVerbatimStringLiteralExpressionSyntax(string value, bool checkNewLine = true) {
      char equals = Tokens.Equals[0];
      int count = 0;
      while (true) {
        string equalsToken = new string(equals, count);
        if (value.StartsWith(equalsToken + OpenBracket)) {
          ++count;
          continue;
        }

        if (value.EndsWith(equalsToken + CloseBracket)) {
          ++count;
          continue;
        }

        if (value.Contains(OpenBracket + equalsToken + OpenBracket)) {
          ++count;
          continue;
        }

        if (value.Contains(CloseBracket + equalsToken + CloseBracket)) {
          ++count;
          continue;
        }

        break;
      }
      if (checkNewLine) {
        if (value.Length > 0 && value[0] == '\n') {
          value = '\n' + value;
        }
      }
      Text = value;
      EqualsCount = count;
    }

    internal override void Render(LuaRenderer renderer) {
      renderer.Render(this);
    }
  }

  public class LuaConstLiteralExpression : LuaLiteralExpressionSyntax {
    public LuaLiteralExpressionSyntax Value { get; }
    public string OpenComment => Tokens.OpenLongComment;
    public string IdentifierToken { get; }
    public string CloseComment => Tokens.CloseDoubleBrace;

    public LuaConstLiteralExpression(string value, string identifierToken) : this(new LuaIdentifierLiteralExpressionSyntax(value), identifierToken) {
    }

    public LuaConstLiteralExpression(LuaLiteralExpressionSyntax value, string identifierToken) {
      Value = value;
      IdentifierToken = identifierToken;
    }

    public override string Text {
      get {
        return Value.Text;
      }
    }

    internal override void Render(LuaRenderer renderer) {
      renderer.Render(this);
    }
  }

  public sealed class LuaCharacterLiteralExpression : LuaConstLiteralExpression {
    public LuaCharacterLiteralExpression(char character) : base(((int)character).ToString(CultureInfo.InvariantCulture), GetIdentifierToken(character)) {
    }

    private static string GetIdentifierToken(char character) {
      return SyntaxFactory.Literal(character).Text;
    }
  }

  public abstract class LuaNumberLiteralExpressionSyntax : LuaLiteralExpressionSyntax {
    public abstract double Number { get; }
    public static readonly LuaNumberLiteralExpressionSyntax Zero = 0;
    public static readonly LuaNumberLiteralExpressionSyntax ZeroFloat = new LuaDoubleLiteralExpressionSyntax(0.0, true);

    internal override void Render(LuaRenderer renderer) {
      renderer.Render(this);
    }

    // Lua reads 4 as an integer and 4.0 as a float, and does integer arithmetic on the first (it wraps past 2^31): a whole float
    // of the C# code is written with a point. A text with an exponent, Infinity or NaN is left as it is. A number the compiler
    // makes itself (a default 0, a loop's step, an index) goes through the implicit operators and is written as it is.
    internal static string WithPoint(string text) {
      foreach (char c in text) {
        if (c != '-' && !char.IsDigit(c)) {
          return text;
        }
      }
      return text + ".0";
    }

    public static implicit operator LuaNumberLiteralExpressionSyntax(float number) {
      return new LuaFloatLiteralExpressionSyntax(number);
    }

    public static implicit operator LuaNumberLiteralExpressionSyntax(double number) {
      return new LuaDoubleLiteralExpressionSyntax(number);
    }
  }

  public sealed class LuaFloatLiteralExpressionSyntax : LuaNumberLiteralExpressionSyntax {
    private readonly float number_;
    private readonly bool isFloatType_;

    /// <param name="isFloatType">The number is a float of the C# code, so a whole one is written with a point.</param>
    public LuaFloatLiteralExpressionSyntax(float number, bool isFloatType = false) {
      number_ = number;
      isFloatType_ = isFloatType;
    }

    public override double Number {
      get {
        return number_;
      }
    }

    public override string Text {
      get {
        var text = number_.ToString("G9", CultureInfo.InvariantCulture);
        return isFloatType_ ? WithPoint(text) : text;
      }
    }
  }

  public sealed class LuaDoubleLiteralExpressionSyntax : LuaNumberLiteralExpressionSyntax {
    public override double Number { get;}
    private readonly bool isFloatType_;

    /// <param name="isFloatType">The number is a float or double of the C# code, so a whole one is written with a point.</param>
    public LuaDoubleLiteralExpressionSyntax(double number, bool isFloatType = false) {
      Number = number;
      isFloatType_ = isFloatType;
    }

    public override string Text {
      get {
        var text = Number.ToString("G17", CultureInfo.InvariantCulture);
        return isFloatType_ ? WithPoint(text) : text;
      }
    }
  }
}
