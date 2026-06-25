// site-layout.js
(function () {
    'use strict';

    const sidebar = document.getElementById('sidebar');
    const overlay = document.getElementById('sidebarOverlay');
    const hamburgerBtn = document.getElementById('hamburgerBtn');

    // Abrir / cerrar sidebar
    function toggleSidebar() {
        sidebar.classList.toggle('open');
        overlay.classList.toggle('active');
    }

    // Cerrar sidebar
    function closeSidebar() {
        sidebar.classList.remove('open');
        overlay.classList.remove('active');
    }

    // Evento del botón hamburguesa
    if (hamburgerBtn) {
        hamburgerBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            toggleSidebar();
        });
    }

    // Cerrar al hacer clic en el overlay
    if (overlay) {
        overlay.addEventListener('click', function () {
            closeSidebar();
        });
    }

    // Cerrar al redimensionar ventana a >= 992px
    window.addEventListener('resize', function () {
        if (window.innerWidth >= 992) {
            closeSidebar();
        }
    });

    // Cerrar al hacer clic en un enlace del sidebar (en móvil)
    const navLinks = document.querySelectorAll('.sidebar-nav .nav-link');
    navLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            if (window.innerWidth < 992) {
                closeSidebar();
            }
        });
    });

    // Resaltar enlace activo según la URL actual
    const currentPath = window.location.pathname;
    navLinks.forEach(function (link) {
        const href = link.getAttribute('href');
        if (href && currentPath.includes(href)) {
            document.querySelectorAll('.sidebar-nav .nav-link').forEach(function (el) {
                el.classList.remove('active');
            });
            link.classList.add('active');
        }
    });
})();