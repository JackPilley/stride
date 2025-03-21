// ==========================================================
// FreeImage 3 .NET wrapper
// Original FreeImage 3 functions and .NET compatible derived functions
//
// Design and implementation by
// - Jean-Philippe Goerke (jpgoerke@users.sourceforge.net)
// - Carsten Klein (cklein05@users.sourceforge.net)
//
// Contributors:
// - David Boland (davidboland@vodafone.ie)
//
// Main reference : MSDN Knowlede Base
//
// This file is part of FreeImage 3
//
// COVERED CODE IS PROVIDED UNDER THIS LICENSE ON AN "AS IS" BASIS, WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, WITHOUT LIMITATION, WARRANTIES
// THAT THE COVERED CODE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE
// OR NON-INFRINGING. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE COVERED
// CODE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT, YOU (NOT
// THE INITIAL DEVELOPER OR ANY OTHER CONTRIBUTOR) ASSUME THE COST OF ANY NECESSARY
// SERVICING, REPAIR OR CORRECTION. THIS DISCLAIMER OF WARRANTY CONSTITUTES AN ESSENTIAL
// PART OF THIS LICENSE. NO USE OF ANY COVERED CODE IS AUTHORIZED HEREUNDER EXCEPT UNDER
// THIS DISCLAIMER.
//
// Use at your own risk!
// ==========================================================

// ==========================================================
// CVS
// $Revision: 1.5 $
// $Date: 2008/11/05 13:19:06 $
// $Id: FIICCPROFILE.cs,v 1.5 2008/11/05 13:19:06 cklein05 Exp $
// ==========================================================

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FreeImageAPI
{
	/// <summary>
	/// This Structure contains ICC-Profile data.
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct FIICCPROFILE
	{
		/// <summary>
		/// Creates a new ICC-Profile for <paramref name="dib"/>.
		/// </summary>
		/// <param name="dib">Handle to a FreeImage bitmap.</param>
		/// <param name="data">The ICC-Profile data.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="dib"/> is null.</exception>
		public FIICCPROFILE(FIBITMAP dib, byte[] data)
			: this(dib, data, data.Length)
		{
		}

		/// <summary>
		/// Creates a new ICC-Profile for <paramref name="dib"/>.
		/// </summary>
		/// <param name="dib">Handle to a FreeImage bitmap.</param>
		/// <param name="data">The ICC-Profile data.</param>
		/// <param name="size">Number of bytes to use from data.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="dib"/> is null.</exception>
		public unsafe FIICCPROFILE(FIBITMAP dib, byte[] data, int size)
		{
			if (dib.IsNull)
			{
				throw new ArgumentNullException("dib");
			}
			
			size = Math.Min(size, data.Length);
			FIICCPROFILE prof = *(FIICCPROFILE*)FreeImage.CreateICCProfile(dib, data, size);
			this.Flags = prof.Flags;
			this.Size = prof.Size;
			this.DataPointer = prof.DataPointer;
		}

		/// <summary>
		/// Info flag of the profile.
		/// </summary>
		public ICC_FLAGS Flags { get; }

		/// <summary>
		/// Profile's size measured in bytes.
		/// </summary>
		public uint Size { get; }

		/// <summary>
		/// Points to a block of contiguous memory containing the profile.
		/// </summary>
		public IntPtr DataPointer { get; }

		/// <summary>
		/// Copy of the ICC-Profiles data.
		/// </summary>
		public unsafe byte[] Data
		{
			get
			{
				byte[] result = new byte[Size];

				ref byte dst = ref result[0];
				ref byte src = ref Unsafe.AsRef<byte>((void*) DataPointer);

				Unsafe.CopyBlockUnaligned(ref dst, ref src, Size);

				return result;
			}
		}

		/// <summary>
		/// Indicates whether the profile is CMYK.
		/// </summary>
		public bool IsCMYK
		{
			get
			{
				return ((Flags & ICC_FLAGS.FIICC_COLOR_IS_CMYK) != 0);
			}
		}
	}
}
