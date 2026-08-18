// Hearth.UI/wwwroot/js/plaid.js
window.openPlaidLinkModal = (linkToken, dotNetHelper) => {
    try {
        const handler = Plaid.create({
            token: linkToken,
            onSuccess: async (publicToken, metadata) => {
                try {
                    await dotNetHelper.invokeMethodAsync('OnPlaidLinkSuccess', publicToken);
                } catch (err) {
                    console.error("Error invoking OnPlaidLinkSuccess:", err);
                }
            },
            onExit: async (error, metadata) => {
                if (error) {
                    try {
                        await dotNetHelper.invokeMethodAsync(
                            'OnPlaidLinkError',
                            `${JSON.stringify(error)} | metadata: ${JSON.stringify(metadata)}`
                        );
                    } catch (err) {
                        console.error("Error invoking OnPlaidLinkError:", err);
                    }
                }
            }
        });
        handler.open();
    } catch (plaidError) {
        console.error("Error initializing Plaid Link:", plaidError);
        dotNetHelper.invokeMethodAsync('OnPlaidLinkError', `Init error: ${JSON.stringify(plaidError)}`);
    }
};