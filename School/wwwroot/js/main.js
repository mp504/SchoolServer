import jQuery from 'jquery';
window.$ = jQuery;

$(document).ready(function () {
    // Counter animation
    $('.counter').each(function () {
        const $this = $(this);
        const target = parseInt($this.data('target'));

        $({ Counter: 0 }).animate({
            Counter: target
        }, {
            duration: 2000,
            easing: 'swing',
            step: function () {
                $this.text(Math.ceil(this.Counter));
            }
        });
    });

    // Notification toggle
    $('.notifications').click(function () {
        // Add notification panel toggle functionality here
        alert('Notifications clicked!');
    });

    // Action buttons
    $('.action-btn').click(function () {
        const action = $(this).text();
        alert(`${action} clicked! This would open a form in a production environment.`);
    });

    // Add hover effect to event cards
    $('.event-card').hover(
        function () {
            $(this).css('transform', 'translateY(-2px)');
            $(this).css('transition', 'transform 0.3s ease');
        },
        function () {
            $(this).css('transform', 'translateY(0)');
        }
    );
});
