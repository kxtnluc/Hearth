//async function openPlaidLink(linkToken) {
//    const handler = Plaid.create({
//        token: linkToken,
//        onSuccess: async (publicToken) => {
//            // Call .NET method with public token
//            await DotNet.invokeMethodAsync('Hearth', 'ExchangePublicToken', publicToken);
//            alert('Bank account successfully linked!');
//        },
//        onExit: (error, metadata) => {
//            if (error) {
//                console.error("Plaid Link error:", error);
//                await DotNet.invokeMethodAsync('Hearth', 'LogTheBadError', JSON.stringify(error));
//            }
//        }
//    });
//    handler.open();
//}

//async function openPlaidLink(linkToken) {
//    console.log("Invoking .NET method...");
//    await DotNet.invokeMethodAsync('Hearth', 'LogTheBadError', 'Test error message');
//}

async function openPlaidLink(linkToken) {
    console.log("Initializing Plaid Link with token:", linkToken);

    try {
        const handler = Plaid.create({
            token: linkToken,
            onSuccess: async (publicToken) => {
                console.log("Plaid Link success, public token:", publicToken);

                try {
                    // Call .NET method with the public token
                    await DotNet.invokeMethodAsync('Hearth', 'ExchangePublicToken', publicToken);
                    alert('Bank account successfully linked!');
                } catch (dotNetError) {
                    console.error("Error invoking .NET method:", dotNetError);
                }
            },
            onExit: (error, metadata) => {
                if (error) {
                    console.error("Plaid Link error:", error);
                    console.log("Metadata:", metadata);

                    // Log the error to .NET
                    DotNet.invokeMethodAsync('Hearth', 'LogTheBadError', `1: ${JSON.stringify(error)}, metadata: ${JSON.stringify(metadata)}`);
                } else {
                    console.log("Plaid Link exited without error:", metadata);
                }
            }
        });

        handler.open();
    } catch (plaidError) {
        console.error("Error initializing Plaid Link:", plaidError);

        // Log the error to .NET
        DotNet.invokeMethodAsync('Hearth', 'LogTheBadError', `2: ${JSON.stringify(error)}`);
    }
}

