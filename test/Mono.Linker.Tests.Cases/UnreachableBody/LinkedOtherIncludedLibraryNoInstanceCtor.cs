using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.UnreachableBody
{
	[Define ("OTHER_INCLUDED")]
	[SetupLinkerArgument ("-r", "other")]
	[SetupCompileBefore ("other.dll", new[] { "Dependencies/OtherAssemblyNoInstanceCtor.il" })]
	[KeptMemberInAssembly ("other.dll", "Mono.Linker.Tests.Cases.UnreachableBody.Dependencies.OtherAssemblyNoInstanceCtor/Foo", "Method()")]
	[RemovedMemberInAssembly ("other.dll", "Mono.Linker.Tests.Cases.UnreachableBody.Dependencies.OtherAssemblyNoInstanceCtor/Foo", "UsedByMethod()")]
	[KeptMemberInAssembly ("other.dll", "Mono.Linker.Tests.Cases.UnreachableBody.Dependencies.OtherAssemblyNoInstanceCtor", "UnusedSanityCheck()")]
	[SetupLinkerArgument ("--enable-opt", "unreachablebodies")]
	public class LinkedOtherIncludedLibraryNoInstanceCtor
	{
		public static void Main ()
		{
#if OTHER_INCLUDED
			UsedToMarkMethod (null);
#endif
		}

#if OTHER_INCLUDED
		[Kept]
		static void UsedToMarkMethod (Mono.Linker.Tests.Cases.UnreachableBody.Dependencies.OtherAssemblyNoInstanceCtor.Foo f)
		{
			f.Method ();
		}
#endif
	}
}