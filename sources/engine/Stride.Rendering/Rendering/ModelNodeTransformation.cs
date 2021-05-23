// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;
using Stride.Core.Mathematics;

namespace Stride.Rendering
{
    [DataContract]
    public struct ModelNodeTransformation
    {
        public int ParentIndex;

        public TransformTRS Transform;

        public Matrix LocalMatrix;

        public Matrix WorldMatrix;

        public bool IsScalingNegative;

        /// <summary>
        /// The flags of this node.
        /// </summary>
        public ModelNodeFlags Flags;

        internal bool RenderingEnabledRecursive;

        /// <summary>
        /// This rotation is not updated by the animation system and can be used by scripts to influence the rotation property of
        /// <see cref="ModelNodeTransformation.Transform"/>. The behaviour is controlled using the <see cref="ModelNodeFlags.CustomRotationOverride"/> flag.
        /// </summary>
        public Quaternion CustomRotation;
    }
}
