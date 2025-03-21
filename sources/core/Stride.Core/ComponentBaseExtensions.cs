// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Annotations;

namespace Stride.Core;

/// <summary>
/// Extensions for <see cref="IComponent"/>.
/// </summary>
public static class ComponentBaseExtensions
{
    /// <summary>
    /// Keeps a disposable object alive by adding it to a container.
    /// </summary>
    /// <typeparam name="T">A component</typeparam>
    /// <param name="thisArg">The component to keep alive.</param>
    /// <param name="container">The container that will keep a reference to the component.</param>
    /// <returns>The same component instance</returns>
    public static T? DisposeBy<T>(this T thisArg, ICollectorHolder container)
        where T : IDisposable
    {
        if (ReferenceEquals(thisArg, null))
            return default;
        return container.Collector.Add(thisArg);
    }

    /// <summary>
    /// Removes a disposable object that was being kept alive from a container.
    /// </summary>
    /// <typeparam name="T">A component</typeparam>
    /// <param name="thisArg">The component to remove.</param>
    /// <param name="container">The container that kept a reference to the component.</param>
    public static void RemoveDisposeBy<T>(this T thisArg, ICollectorHolder container)
        where T : IDisposable
    {
        if (ReferenceEquals(thisArg, null))
            return;
        container.Collector.Remove(thisArg);
    }

    /// <summary>
    /// Keeps a referencable object alive by adding it to a container.
    /// </summary>
    /// <typeparam name="T">A component</typeparam>
    /// <param name="thisArg">The component to keep alive.</param>
    /// <param name="container">The container that will keep a reference to the component.</param>
    /// <returns>The same component instance</returns>
    public static T? ReleaseBy<T>(this T thisArg, ICollectorHolder container)
        where T : IReferencable
    {
        if (ReferenceEquals(thisArg, null))
            return default;
        return container.Collector.Add(thisArg);
    }

    /// <summary>
    /// Removes a referencable object that was being kept alive from a container.
    /// </summary>
    /// <typeparam name="T">A component</typeparam>
    /// <param name="thisArg">The component to remove.</param>
    /// <param name="container">The container that kept a reference to the component.</param>
    public static void RemoveReleaseBy<T>(this T thisArg, ICollectorHolder container)
        where T : IReferencable
    {
        if (ReferenceEquals(thisArg, null))
            return;
        container.Collector.Remove(thisArg);
    }

    /// <summary>
    /// Pins this component as a new reference.
    /// </summary>
    /// <typeparam name="T">A component</typeparam>
    /// <param name="thisArg">The component to add a reference to.</param>
    /// <returns>This component.</returns>
    /// <remarks>This method is equivalent to call <see cref="IReferencable.AddReference"/> and return this instance.</remarks>
    public static T? KeepReference<T>(this T thisArg)
        where T : IReferencable
    {
        if (ReferenceEquals(thisArg, null))
            return default;
        thisArg.AddReference();
        return thisArg;
    }

    /// <summary>
    /// Pushes a tag to a component and restore it after using it. See remarks for usage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="component">The component.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>PropertyTagRestore&lt;T&gt;.</returns>
    /// <remarks>
    /// This method is used to set save a property value from <see cref="ComponentBase.Tags"/>, set a new value
    /// and restore it after. The returned object must be disposed once the original value must be restored.
    /// </remarks>
    public static PropertyTagRestore<T> PushTagAndRestore<T>(this ComponentBase component, PropertyKey<T> key, T value)
    {
        // TODO: Not fully satisfied with the name and the extension point (on ComponentBase). We need to review this a bit more
        var restorer = new PropertyTagRestore<T>(component, key);
        component.Tags.Set(key, value);
        return restorer;
    }

    /// <summary>
    /// Struct PropertyTagRestore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PropertyTagRestore<T> : IDisposable
    {
        private readonly ComponentBase container;

        private readonly PropertyKey<T> key;

        private readonly T previousValue;

        public PropertyTagRestore(ComponentBase container, PropertyKey<T> key)
            : this()
        {
            ArgumentNullException.ThrowIfNull(container);
            ArgumentNullException.ThrowIfNull(key);
            this.container = container;
            this.key = key;
            previousValue = container.Tags.Get(key);
        }

        public readonly void Dispose()
        {
            // Restore the value
            container.Tags.Set(key, previousValue);
        }
    }
}
