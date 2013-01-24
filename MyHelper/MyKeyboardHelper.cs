using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace MyHelper4Web
{
    /// <summary>
    /// 提供访问键盘当前状态的属性，
    /// 如什么键当前按下，提供了一种方法，以发送击键到活动窗口。
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public class MyKeyboardHelper
    {
        #region 属性

        /// <summary>获取一个布尔值，表示如果ALT键是向下。</summary>
        /// <returns>一个布尔值：如果ALT键，否则为false。</returns>
        public static bool AltKeyDown
        {
            get { return ((Control.ModifierKeys & Keys.Alt) > Keys.None); }
        }

        /// <summary>获取一个布尔值，指示，如果已开启CAPS LOCK键。 </summary>
        /// <returns>一个布尔值：如果已开启CAPS LOCK键，则为true，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool CapsLock
        {
            get { return ((UnsafeNativeMethods.GetKeyState(20) & 1) > 0); }
        }

        /// <summary>获取一个布尔值，表示如果CTRL键是向下。</summary>
        /// <returns>一个布尔值。真正如果CTRL键，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool CtrlKeyDown
        {
            get { return ((Control.ModifierKeys & Keys.Control) > Keys.None); }
        }

        /// <summary>获取一个布尔值，表示如果NUM LOCK键是。</summary>
        /// <returns>一个布尔值。true，如果数字锁定，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool NumLock
        {
            get { return ((UnsafeNativeMethods.GetKeyState(0x90) & 1) > 0); }
        }

        /// <summary>获取一个布尔值，指示是否SCROLL LOCK键是。 </summary>
        /// <returns>一个布尔值。True如果滚动锁被，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool ScrollLock
        {
            get { return ((UnsafeNativeMethods.GetKeyState(0x91) & 1) > 0); }
        }

        /// <summary>获取一个布尔值，表示如果SHIFT键是向下。</summary>
        /// <returns>一个布尔值。真正如果SHIFT键是向下，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool ShiftKeyDown
        {
            get { return ((Control.ModifierKeys & Keys.Shift) > Keys.None); }
        }

        #endregion

        #region Methods

        /// <summary>发送一个或多个击键到活动窗口，如果在键盘上输入。</summary>
        /// <param name="keys">一个字符串，定义发送键。</param>
        public static void SendKeys(string keys)
        {
            SendKeys(keys, false);
        }

        /// <summary>发送一个或多个击键到活动窗口，如果在键盘上输入。</summary>
        /// <param name="keys">一个字符串，定义发送键。</param>
        /// <param name="wait">可选的。一个布尔值，指定是否等待的应用程序继续之前得到处理的击键。默认为true。</param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        public static void SendKeys(string keys, bool wait)
        {
            if (wait)
            {
                System.Windows.Forms.SendKeys.SendWait(keys);
            }
            else
            {
                System.Windows.Forms.SendKeys.Send(keys);
            }
        }

        #endregion
    }

    /// <summary>
    /// 从 .Net 2.0 的 System.Windows.Forms.Dll 库提取
    /// 版权所有：微软公司
    /// </summary>
    internal static class NativeMethodsTemp
    {
        [StructLayout(LayoutKind.Sequential)]
        public sealed class TagDVTARGETDEVICE
        {
            [MarshalAs(UnmanagedType.U4)] public int tdSize;
            [MarshalAs(UnmanagedType.U2)] public short tdDriverNameOffset;
            [MarshalAs(UnmanagedType.U2)] public short tdDeviceNameOffset;
            [MarshalAs(UnmanagedType.U2)] public short tdPortNameOffset;
            [MarshalAs(UnmanagedType.U2)] public short tdExtDevmodeOffset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public COMRECT()
            {
            }

            public COMRECT(Rectangle r)
            {
                left = r.X;
                top = r.Y;
                right = r.Right;
                bottom = r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return
                    string.Concat(new object[] {"Left = ", left, " Top ", top, " Right = ", right, " Bottom = ", bottom});
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class TagLOGPALETTE
        {
            [MarshalAs(UnmanagedType.U2)] public short palVersion;
            [MarshalAs(UnmanagedType.U2)] public short palNumEntries;
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        public static Guid IidIViewObject = new Guid("{0000010d-0000-0000-C000-000000000046}");

        [ComImport, Guid("0000010d-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject
        {
            [PreserveSig]
            int Draw([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect,
                     [In] NativeMethodsTemp.TagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw,
                     [In] NativeMethodsTemp.COMRECT lprcBounds, [In] NativeMethodsTemp.COMRECT lprcWBounds,
                     IntPtr pfnContinue, [In] int dwContinue);

            [PreserveSig]
            int GetColorSet([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect,
                            [In] NativeMethodsTemp.TagDVTARGETDEVICE ptd, IntPtr hicTargetDev,
                            [Out] NativeMethodsTemp.TagLOGPALETTE ppColorSet);

            [PreserveSig]
            int Freeze([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect,
                       [Out] IntPtr pdwFreeze);

            [PreserveSig]
            int Unfreeze([In, MarshalAs(UnmanagedType.U4)] int dwFreeze);

            void SetAdvise([In, MarshalAs(UnmanagedType.U4)] int aspects, [In, MarshalAs(UnmanagedType.U4)] int advf,
                           [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);

            void GetAdvise([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] paspects,
                           [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] advf,
                           [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);
        }

        /// <summary>
        /// retrieves the status of the specified virtual key
        /// </summary>
        /// <param name="keyCode">Specifies a virtual key</param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows: 
        ///  If the high-order bit is 1, the key is down; otherwise, it is up.
        ///  If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, 
        ///  is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0.
        ///  A toggle key's indicator light (if any) on the keyboard will be on when the key is
        ///  toggled, and off when the key is untoggled.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetKeyState(int keyCode);
    }
}