using System.Runtime.InteropServices;
using Xunit;
using Xunit.Extensions;

namespace System.Collections.Tests
{
    public class NativeLibraryTests
    {

        [Fact]
        public void TryLoad_And_Free()
        {
            Assert.True(NativeLibrary.TryLoad(@"C:\windows\System32\user32.dll", out var hmodule));
            NativeLibrary.Free(hmodule);
        }

        [Fact]
        public void Load_And_Free()
        {
            var hmodule = NativeLibrary.Load(@"C:\windows\System32\user32.dll");
            NativeLibrary.Free(hmodule);
        }

        [Fact]
        public void TryLoad_NotFound()
        {
            Assert.False(NativeLibrary.TryLoad(@"C:\windows\System32\foobar.dll", out _));
        }

        [Fact]
        public void Load_NotFound()
        {
            Assert.Throws<DllNotFoundException>(() =>
            {
                var hmodule = NativeLibrary.Load(@"C:\windows\System32\foobar.dll");
            });
        }

        [Fact]
        public void Load_BadImage()
        {
            Assert.Throws<BadImageFormatException>(() =>
            {
                var hmodule = NativeLibrary.Load(IntPtr.Size == 8 ? @"C:\windows\SysWow64\kernel32.dll" : @"C:\windows\Sysnative\kernel32.dll");
            });
        }

    }
}
