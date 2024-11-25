# space

a custom language

how to use

```
;; single line comment

;. multi
   line
   comment .;

;. types

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

v2     Vector 2 (signed 2 dimensional float)
v3     Vector 3 (signed 3 dimenstional float)
v4     Vector 4

uv2    Unsigned Vector 2 (2 dimensional unsigned float)
uv3    Unsigned Vector 3
uv4    Unsigned Vector 4

iv2    Integer Vector 2 (signed 2 dimensional integer)
iv3    Integer Vector 3
iv4    Integer Vector 4

uiv2   Unsigned Integer Vector 2
uiv3   Unsigned Integer Vector 3
uiv4   Unsigned Integer Vector 4

.;

;. keywords

enm    enum

fn     function
rfn    function with a return type

.;

;. operators

+     plus
-     minus
*     multiply
/     divide

unary

-     negative

.;

;; "hello_world.orbt"

;; space represents making a class

space helloworld {
    ;; fn makes a non returning function
    fn main() {
        ;; spk works just like writeline

        ;; makes a new var of type string and sets the value to the function's return
        texttosay: str :> text("World")

        spk(texttosay)

        ;;for loop example
        for(i: i32, 0,100)
            spk(;"hi {i}")

        ;;backwards for loop
        for(i: i32 :> 100, >0, --)
            spk(;"bye {i}")
    }

    ;; rfn makes a function with a return type noted after rfn
    rfn str text(txt: str) {
        ;; typing i before the string makes it concat the string with the inputs
        ;; string concats allow for string inputs aswel
        ret i"Hello {txt}!"
    }

    ;; fibonacci example
    rfn i32 fib(n: i32) {
        ret (n < 2) :: n ` fib(n-1)+fib(n-2)
    }

    ;; example of func on one line
    fn 1plus_a(r -> a: i32) :> a++

    ;.
        can be

        fn 1plus_a(r -> a: i32) :> a++

        or

        fn 1plus_a(r -> a: i32) :> 
            a++

        or

        fn 1plus_a(r -> a: i32) 
            :> a++
    .;
}
```