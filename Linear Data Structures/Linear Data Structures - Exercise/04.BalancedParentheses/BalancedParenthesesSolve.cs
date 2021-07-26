namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        private Stack<char> stack;

        public BalancedParenthesesSolve()
        {
            this.stack = new Stack<char>();
        }

        public bool AreBalanced(string parentheses)
        {
            if(parentheses.Length % 2 != 0)
            {
                return false;
            }
             
            for (int i = 0; i < parentheses.Length; i++)
            {
                if(parentheses[i] == '(' || parentheses[i] == '[' || parentheses[i] == '{')
                {
                    this.stack.Push(parentheses[i]);
                }
                else
                {
                    if (this.stack.Peek() == '(' && parentheses[i] == ')'
                    || this.stack.Peek() == '[' && parentheses[i] == ']'
                    || this.stack.Peek() == '{' && parentheses[i] == '}')
                    {
                        this.stack.Pop();
                    }
                }
            }

            if (stack.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
