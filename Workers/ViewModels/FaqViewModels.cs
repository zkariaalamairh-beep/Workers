namespace Workers.ViewModels;

public class FaqPageViewModel
{
    public List<FaqCategoryViewModel> Categories { get; set; }
        = new();
}

public class FaqCategoryViewModel
{
    public string Title { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public List<FaqItemViewModel> Questions { get; set; }
        = new();
}

public class FaqItemViewModel
{
    public string Id { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;
}