# space

a custom language

how to use

```
~~ single line comment

~{ multi
   line
   comment }~

~{ types

ui8     unsigned 8 bit integer
ui16    unsigned 16 bit integer
ui32    unsigned 32 bit integer
ui64    unsigned 64 bit integer

i8     signed 8 bit integer
i16    signed 16 bit integer
i32    signed 32 bit integer
i64    signed 64 bit integer

f16    16 bit float
f32    32 bit float
f64    64 bit float

bool   boolean
str    string
chr    character

(for the next section, you can replace N with any of the bit sizes of the above numbers for the specified type)

fNv2   Vector 2 (signed 2 dimensional float)
fNv3   Vector 3 (signed 3 dimenstional float)
fNv4   Vector 4

iNv2   Integer Vector 2 (signed 2 dimensional integer)
iNv3   Integer Vector 3
iNv4   Integer Vector 4

uiNv2  Unsigned Integer Vector 2
uiNv3  Unsigned Integer Vector 3
uiNv4  Unsigned Integer Vector 4

}~

~{ keywords

enum   enum

fn     function
rfn    function with a return type

}~

~{ operators

+     plus
-     minus
*     multiply
/     divide

unary

-     negative

}~

~~ "hello_world.orbt"

~~ space represents making a class

space helloworld {
    ~~ fn makes a non returning function
    fn main() {
        ~~ spk works just like writeline

        ~~ makes a new var of type string and sets the value to the function's return
        texttosay: str :> text("World")

        spk(texttosay)

        ~~ say hi, but only say even numbers
        for(i: i32, 0 ~> 100, 2)
            spk(f"hi {i}")

        ~~ backwards for loop
        for(i: i32, 100 <~ 0)
            spk(f"bye {i}")

        a: i64 :> 1

        ~~ square a and subtract 1 from a every time
        for(i: i32, 0 ~> 100) { ~~ would be the same as 'for(int i = 0; i < 100; i++)' in c#
            ~~ set a to 'a *= a'
            a :> * a
            ~~ subtract from a
            a :> --
            spk(a)
        }

        ~~ make a 2D array of integers with size 3,2
        map: i32^2[3,2]

        for(x: i8, 0 ~> map.len(0))
            for(y: i8, 0 ~> map.len(1))
                ~~ set map[x,y] to the sin of x/y
                map[x,y] :> m.sin(x/y)
    }

    ~~ rfn makes a function with a return type noted after rfn
    rfn str text(text: str) {
        ~~ putting f makes it so you can do "text {input} text {input}"
        ret f"Hello {text}!"
    }

    ~~ fibonacci example
    rfn i32 fib(n: i32) {
        ret (n < 2) :: n ` fib(n-1)+fib(n-2)
    }

    ~~ example of func on one line
    fn 1plus_a(&a: i32) :> a :> ++

    ~{
        can be

        fn 1plus_a(&a: i32) :> a :> ++

        or

        fn 1plus_a(&a: i32) :> 
            a :> ++

        or

        fn 1plus_a(&a: i32) 
            :> a :> ++
    }~
}
```