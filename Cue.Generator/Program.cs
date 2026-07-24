using Cue.Generator;
using Cuelang.Cue;

using var ctx = new CueContext();
using var value = ctx.Compile("""
                              x : {
                                  @foo()
                                  @bar(baz)

                                  y: int @qux(quux)
                                  z: [1, 2, 3]
                              }
                              """);

using var x = value.Lookup("x");
var node = x.ToCueValueNode();

Console.WriteLine(node is CueStructValue);
