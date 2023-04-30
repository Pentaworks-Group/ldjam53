using System;

namespace Assets.Scripts.Extensions
{
    public static class StringExtensions
    {
        public static Core.Definitions.ElementType GetElementType(this String elementTypeReference)
        {
            if (Base.Core.Game?.State?.GameMode?.ElementTypes?.TryGetValue(elementTypeReference, out var elementType) == true)
            {
                return elementType;
            }

            return default;
        }
    }
}
