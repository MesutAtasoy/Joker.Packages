﻿using System.Reflection;
using static Joker.DispatchProxy.BaseDispatchProxyAsync.AsyncDispatchProxyGenerator;

namespace Joker.DispatchProxy.BaseDispatchProxyAsync;

public abstract class DispatchProxyAsync
{
    public static T Create<T, TProxy>() where TProxy : DispatchProxyAsync
    {
        return (T)CreateProxyInstance(typeof(TProxy), typeof(T));
    }

    public abstract object Invoke(MethodInfo method, object[] args);

    public abstract Task InvokeAsync(MethodInfo method, object[] args);

    public abstract Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args);
}