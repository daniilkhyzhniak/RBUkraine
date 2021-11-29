async function onSignIn(googleUser) {
    await fetch(`https://localhost:5001/login/google?token=${googleUser.wc.id_token}`,
            {
                method: "post"
            })
        .then(async () => {
            const auth2 = gapi.auth2.getAuthInstance();
            await auth2.signOut();
            location.href = "https://localhost:5001";
        });

}