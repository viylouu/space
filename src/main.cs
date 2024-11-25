internal class main {
    static void Main() {
        bool tree = false;

        while(true) {
            Console.Write("> ");
            var line = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(line))
                return;

            if(line == "#tree") {
                tree = !tree;
                Console.WriteLine(tree ? "showing trees" : "not showing trees");
                continue;
            } else if(line == "#clr") {
                Console.Clear();
                continue;
            }

            var synTree = syntree.parse(line);
            var bndr = new binder();
            var bndexpr = bndr.bindexpr(synTree.root);

            var diags = synTree.diags.Concat(bndr.diags).ToArray();

            //1:05:43

            if(tree) {
                Console.ForegroundColor = ConsoleColor.DarkGray;

                prettyprint(synTree.root);

                Console.ResetColor();
            }

            if(!diags.Any()) {
                var e = new evaler(bndexpr);
                var res = e.eval();
                Console.WriteLine(res);
            } else {
                Console.ForegroundColor = ConsoleColor.Red;

                foreach(var diag in synTree.diags)
                    Console.WriteLine(diag);

                Console.ResetColor();
            }
        }
    }

    static void prettyprint(synnode node, string indent = "", bool isLast = true) {
        var marker = isLast ? "└──" : "├──";

        Console.Write(indent);
        Console.Write(marker);
        Console.Write(node.type);

        if(node is syntok t && t.val != null) {
            Console.Write(" ");
            Console.Write(t.val);
        }
        
        Console.WriteLine();

        var lastchild = node.getchildren().LastOrDefault();

        indent += isLast?"   ":"│  ";

        foreach(var child in node.getchildren())
            prettyprint(child, indent, child==lastchild);
    }
}