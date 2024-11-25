abstract class synnode {
    public abstract syntype type { get; }

    public abstract IEnumerable<synnode> getchildren();
}