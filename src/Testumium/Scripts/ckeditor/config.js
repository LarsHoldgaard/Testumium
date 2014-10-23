/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
    config.toolbar = 'Basic';
    config.resize_enabled = false;
    config.toolbar_Basic =
    [
        ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Subscript', 'Superscript'],
        ['Table', 'SpecialChar'],
        ['FontFormat', 'FontName', 'FontSize'], ['TextColor', 'BGColor'], ['OrderedList', 'UnorderedList', '-', 'Outdent', 'Indent', 'Blockquote'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyFull']
    ];

    config.toolbar_Simple =
    [
        ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'SpecialChar'], ['FontFormat', 'FontName', 'FontSize'], ['TextColor', 'BGColor']
    ];
};
