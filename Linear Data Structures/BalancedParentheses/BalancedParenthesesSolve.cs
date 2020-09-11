namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (string.IsNullOrEmpty(parentheses) || parentheses.Length % 2 != 0)
            {
                return false;
            }

            var stack = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                var currParentheses = parentheses[i];
                char expectedParentheses = default;

                if (currParentheses == '(' || currParentheses == '[' || currParentheses == '{')
                {
                    stack.Push(currParentheses);
                    continue;
                }
                else  if (currParentheses == ')')
                {
                    expectedParentheses = '(';
                }
                else if (currParentheses == ']')
                {
                    expectedParentheses = '[';
                }
                else if (currParentheses == '}')
                {
                    expectedParentheses = '{';
                }

                if (stack.Pop() == expectedParentheses)
                {
                    continue;
                }
                else
                {
                    return false;
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
