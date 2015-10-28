(function($) {
  "use strict";
  jQuery(document).ready(function(){
    // Sticky menu
    jQuery(".sticky").sticky({
        topSpacing:0
    });
    // site preloader -- also uncomment the div in the header and the css style for #preloader
    jQuery(window).load(function(){
        jQuery('#preloader').fadeOut('slow',function(){jQuery(this).remove();});
    });
});
})(jQuery);