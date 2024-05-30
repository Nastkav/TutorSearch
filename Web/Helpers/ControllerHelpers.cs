using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Helpers;

public class ControllerHelpers
{
    private readonly IMediator _mediator;

    public ControllerHelpers(IMediator mediator) =>
        _mediator = mediator;

    public async Task<List<SelectListItem>> GetSelectList(IRequest<Dictionary<int, string>> q, string defaultText = "")
    {
        var query = await _mediator.Send(q);
        var list = query
            .Select(o => new SelectListItem { Value = o.Key.ToString(), Text = o.Value })
            .ToList();
        if (defaultText != "")
            list.Insert(0, new SelectListItem(defaultText, "0"));
        return list;
    }

    public void UpdateSelf(List<SelectListItem> inputs, int userId)
    {
        var sserIdStr = userId.ToString();
        var item = inputs.FirstOrDefault(x => x.Value == sserIdStr);
        if (item == null)
            inputs.Insert(0, new SelectListItem { Value = sserIdStr, Text = "Мої" });
        else
            item.Text = "Мої";
    }

    public async Task<byte[]> BytesFromFormFile(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        return ms.ToArray();
    }
}