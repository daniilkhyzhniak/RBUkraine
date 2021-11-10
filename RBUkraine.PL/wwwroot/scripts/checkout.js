let apiKey =
    'pk_test_51JjT6hIYuQyIaasF8HYcE2qFfssinmugSDK6zcibrA///FW5fKPnO4BKWcPxBAPrxWpO1YNK6rpwDY2OJIuOI1rFuVj00AjtbqE2p';
apiKey = apiKey.split('///').join('');
const stripe = Stripe(apiKey);

const redirectToCheckout = sessionId => {
    stripe.redirectToCheckout({
        sessionId: sessionId
    }).then((resp) => {
        console.log(resp);
    });
};