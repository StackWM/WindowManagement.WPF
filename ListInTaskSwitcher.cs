namespace LostTech.Stack.WindowManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Interop;
    using PInvoke;
    using Win32Exception = System.ComponentModel.Win32Exception;

    public static class ListInTaskSwitcher
    {
        public static bool IsListedInTaskSwitcher(this Window window) {
            var helper = new WindowInteropHelper(window);
            var style = (User32.WindowStylesEx)User32.GetWindowLong(helper.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            return style.HasFlag(User32.WindowStylesEx.WS_EX_TOOLWINDOW);
        }

        public static Exception SetIsListedInTaskSwitcher(this Window window, bool list) {
            var helper = new WindowInteropHelper(window);
            var style = (User32.WindowStylesEx)User32.GetWindowLong(helper.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            style = list
                ? style & ~User32.WindowStylesEx.WS_EX_TOOLWINDOW
                : style | User32.WindowStylesEx.WS_EX_TOOLWINDOW;
            return User32.SetWindowLong(helper.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE,
                       (User32.SetWindowLongFlags)style) == 0
                ? new Win32Exception() : null;
        }
    }
}
