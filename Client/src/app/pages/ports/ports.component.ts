import {Component, OnInit} from '@angular/core';
import {Port} from '../../models/port';
import {PortService} from '../../services/port.service';

@Component({
  selector: 'app-ports',
  imports: [],
  templateUrl: './ports.component.html',
  styleUrl: './ports.component.css'
})
export class PortsComponent implements OnInit {
  ports: Port[] = [];

  constructor(private portService: PortService)  {}

  ngOnInit(): void {
    this.portService.getAll().subscribe(data => {
      this.ports = data;
    });
  }
}
