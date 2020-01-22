namespace LostTech.Stack.WindowManagement
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Interop;

    public static class Win32Utils
    {
        static readonly Win32WindowFactory win32WindowFactory = new Win32WindowFactory();
        public static Win32Window GetNativeWindow(this Window window) {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            IntPtr handle = new WindowInteropHelper(window).Handle;
            return win32WindowFactory.Create(handle);
        }
        public static Win32Window? TryGetNativeWindow(this Window window) {
            IntPtr handle = window.TryGetHandle();
            return handle == IntPtr.Zero ? null : win32WindowFactory.Create(handle);
        }
        public static IntPtr TryGetHandle(this Window window) {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            try {
                return new WindowInteropHelper(window).Handle;
            } catch(Exception e) {
                Debug.WriteLine("WARN: Can't get window handle: " + e);
                return IntPtr.Zero;
            }
        }
    }
}
