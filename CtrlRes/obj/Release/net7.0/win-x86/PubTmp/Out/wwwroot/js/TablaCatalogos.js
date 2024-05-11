        $(function () {
            $(".borrar").click(function () {
                var id = $(this).data("id");
                var url = $(this).data("url");
                console.log(url);
                swal({
                    title: "¿Desea eliminar el registro?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Borrar",
                    closeOnconfirm: true
                }, function () {
                    $.ajax({
                        type: 'DELETE',
                        url: url + id,
                        success: function (data) {
                            if (data.success) {
                                toastr.success(data.message);
                                // Encontrar la fila correspondiente al registro eliminado y eliminarla
                                $("button.borrar[data-id='" + id + "']").parents("tr").remove();
                            }
                            else {
                                toastr.error(data.message);
                            }
                        }
                    });
                });
            });
        });


    $(document).ready(function () {
        $('#Tabla tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="Filtrar.." />');
        });

        var table = $('#Tabla').DataTable({
            columnDefs: [
                { targets: '_all', visible: false }
            ],
            initComplete: function (settings, json) {
                // Muestra las columnas cuando se hayan cargado todos los datos
                this.api().columns().visible(true);
            },
            dom: 'Bftlip',
            responsive: true,
            buttons: [
                {
                    extend: 'colvis',
                    text: '<i class="fa fa-eye"></i> Ver columnas',
                    className: 'btn btn-dark',
                    columnText: function (dt, idx, title) {
                        return (idx + 1) + ': ' + title;
                    }
                },
                {
                    extend: 'copy',
                    text: '<i class="fa fa-copy"></i> Copiar',
                    className: 'btn btn-light',
                    exportOptions: {
                        columns: ':visible:not(.not-export-col)'
                    }
                },
                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel"></i> Excel',
                    className: 'btn btn-success',
                    exportOptions: {
                        columns: ':visible:not(.not-export-col)'
                    }
                },
                {
                    extend: 'pdf',
                    text: '<i class="fa fa-file-pdf"></i> PDF',
                    className: 'btn btn-danger',
                    exportOptions: {
                        columns: ':visible:not(.not-export-col)'
                    }
                },
                {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Imprimir',
                    className: 'btn btn-secondary',
                    exportOptions: {
                        columns: ':visible:not(.not-export-col)'
                    }
                },
                {
                    text: '<i class="fa fa-plus"></i> Agregar nuevo registro',
                    className: 'btn btn-success agregar-btn independent-rounded',
                    action: function () {
                        window.location.href = window.location + '/Create';
                    }
                }
            ],
            pagingType: 'full_numbers',
            "paging": true,
            "pageLength": 10,
            "lengthMenu": [5, 10, 25, 50, 100],
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json",
                "lengthMenu": "Mostrar _MENU_ registros  |  "
            },
            "processing": true,
            "deferRender": true,
            "initComplete": function (settings, json) {
                // Muestra las columnas cuando se hayan cargado todos los datos
                this.api().columns().visible(true);
            
                this.api().columns().every(function () {
                    var that = this;

                    $('input', this.footer()).on('keyup change', function () {
                        if (that.search() !== this.value) {
                            that
                                .search(this.value)
                                .draw();
                        }
                    });
                })
            },
        });

    });