﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fuxion.Factories;

namespace Fuxion.Windows.Threading
{
	public interface IInvokable
	{
		bool UseInvoker { get; set; }
	}
	[FactoryDefaultImplementation(typeof(SynchronousInvoker))]
	public interface IInvoker
	{
		Task InvokeActionDelegate(IInvokable invokable, Delegate method, params object[] args);
		Task<TResult> InvokeFuncDelegate<TResult>(IInvokable invokable, Delegate method, params object[] args);
	}
	public class SynchronousInvoker : IInvoker
	{
		public Task InvokeActionDelegate(IInvokable invokable, Delegate method, params object[] args) => Task.FromResult(method.DynamicInvoke(args));
		public Task<TResult> InvokeFuncDelegate<TResult>(IInvokable invokable, Delegate method, params object[] args) => Task.FromResult((TResult) method.DynamicInvoke(args));
	}
	public static class IInvokerExtensions
	{
		public static Task Invoke(this IInvokable me, Action action) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action);
		public static Task Invoke<T>(this IInvokable me, Action<T> action, T param) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param);
		public static Task Invoke<T1, T2>(this IInvokable me, Action<T1, T2> action, T1 param1, T2 param2) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param1, param2);
		public static Task Invoke<T1, T2, T3>(this IInvokable me, Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param1, param2, param3);
		public static Task Invoke<T1, T2, T3, T4>(this IInvokable me, Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param1, param2, param3, param4);
		public static Task Invoke<T1, T2, T3, T4, T5>(this IInvokable me, Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param1, param2, param3, param4, param5);
		public static Task Invoke<T1, T2, T3, T4, T5, T6>(this IInvokable me, Action<T1, T2, T3, T4, T5, T6> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) => Factory.Get<IInvoker>().InvokeActionDelegate(me, action, param1, param2, param3, param4, param5, param6);
		public static Task<TResult> Invoke<TResult>(this IInvokable me, Func<TResult> func) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func);
		public static Task<TResult> Invoke<T, TResult>(this IInvokable me, Func<T, TResult> func, T param) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param);
		public static Task<TResult> Invoke<T1, T2, TResult>(this IInvokable me, Func<T1, T2, TResult> func, T1 param1, T2 param2) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param1, param2);
		public static Task<TResult> Invoke<T1, T2, T3, TResult>(this IInvokable me, Func<T1, T2, T3, TResult> func, T1 param1, T2 param2, T3 param3) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param1, param2, param3);
		public static Task<TResult> Invoke<T1, T2, T3, T4, TResult>(this IInvokable me, Func<T1, T2, T3, T4, TResult> func, T1 param1, T2 param2, T3 param3, T4 param4) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param1, param2, param3, param4);
		public static Task<TResult> Invoke<T1, T2, T3, T4, T5, TResult>(this IInvokable me, Func<T1, T2, T3, T4, T5, TResult> func, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param1, param2, param3, param4, param5);
		public static Task<TResult> Invoke<T1, T2, T3, T4, T5, T6, TResult>(this IInvokable me, Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) => Factory.Get<IInvoker>().InvokeFuncDelegate<TResult>(me, func, param1, param2, param3, param4, param5, param6);
	}
}
