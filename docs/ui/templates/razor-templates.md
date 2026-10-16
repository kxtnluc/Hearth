# Finance Page Template

This is a page template from which most, if not all, Finance pages should be built.
Simply CTRL-H the key tags below, or go through manually, replacing what you need for the given page.
Variables marked with a `__` prefix are meant to be replaced.

### Full Template

```razor
@page "/finance/<page-url>"
@inject IUserService _userService
@inject IJSRuntime JS

@* 
    This is a page Template from which most if not all Finance pages should be built from. 
    Simply CTRL-H the key tags, or go thru manually, replacing what you need for the given page.
    Variabels marked with __ prefix, are meant to be replaced
*@

@* Loading *@

@if (initialRenderComplete == false)
{
    <LoadingCircleIcon AutoCenter="true" />
}

@* Page *@

<main class="main-_pageObject" hidden="@(initialRenderComplete ? false : true)">
    <section class="section-page-title">
        <Title>
            __Page Title
        </Title>
    </section>
    @if (__pageObjects.Count >= 1 || __pageObjects != null)
    {
        @* - Actual Page Content - *@
        <section class="section-01">
            - Actualy Page Content -
        </section>      
    }
    else
    {
        <section class="section-no-records">
            <div class="title-container">
                <Title>No __Content Found...</Title>
            </div>
        </section>
    }
</main>

@* Toolbar *@

<Toolbar HasAddBtn="false"
         HasSearchBtn="true"
         HasManageBtn="false"
         HasSettingsBtn="false"
         HasLinkBtn="true"
         Position="E_TOOLBAR_POSITION.BottomRight"
         Size="E_SIZE.lg"
         TooltipSubject="__Content" />

@code {
    // - Loading -
    bool initialRenderComplete = false;
    bool resizeInitialized = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (initialRenderComplete == false && __pageObjects != null)
            {
                initialRenderComplete = true;
                StateHasChanged(); // Trigger re-render to hide the spinner
            }

        }
    }


    // - Page -
    UserDTO? currentUser = null;

    List<string>? __pageObjects = new();
    List<string>? __alternatePageObjects = new();
    string? __selectedPageObject = "";

    protected override async Task OnInitializedAsync()
    {
        try
        {
            currentUser = await _userService.GetCurrentUser();
            __pageObjects = null;
            __alternatePageObjects = null;
        }
        catch (HearthRecordNotFoundException ex)
        {
            // TODO show error toast
        }
    }

    // - CTO -
    


    // - Filter -

}
```

## Placeholders to Replace

| Placeholder | Description |
| :---------: | ----------: |
| `__Page Title` | The heading shown at the top of the page (e.g. "Transactions") |
| `__pageObjects` | The main list of DTOs the page displays (e.g. `List<TransactionDTO>`) — rename and retype |
| `__alternatePageObjects` | A secondary list, if the page needs one (e.g. filtered vs. unfiltered) — remove if unused |
| `__selectedPageObject` | Tracks a currently-selected item on the page, if applicable — remove if unused |
| `__Content` | Used in the empty-state message and the Toolbar's `TooltipSubject` (e.g. "Transaction", "Account") |
| `- Actualy Page Content -` | Replace with the page's real markup once the list/detail layout is built out |

## Structure Notes

| Section | Description |
| :---------: | ----------: |
| Loading | Shows a `LoadingCircleIcon` until `initialRenderComplete` flips true, then the real page fades in via the `hidden` attribute |
| Empty State | `section-no-records` renders when `__pageObjects` is null or empty, showing a "No Content Found" message instead of a blank page |
| Toolbar | A `Toolbar` docked bottom-right; toggle `HasAddBtn`/`HasManageBtn`/`HasSettingsBtn`/`HasLinkBtn` per page based on what actions that page actually supports |
| `OnAfterRenderAsync` | Flips `initialRenderComplete` once the page's data has loaded, and is the place to wire up any JS interop init calls once real content exists in the DOM |

## CSS Notes

There are a few default CSS classes in this template. 2 of which are stored in app.css, but one which must always be manually added to the razor's css file

app.css
```css
.section-no-records {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 95%;

    .title-container {
        align-self: center;
    }
}
```

.razor.css
```css
.main-transactions {
    height: 100%;
    /*        background-color: palegoldenrod;*/
}
```