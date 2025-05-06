import {Component, OnInit} from '@angular/core';
import {Port} from '../../models/port';
import {PortService} from '../../services/port.service';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'app-ports',
  imports: [
    NgForOf
  ],
  templateUrl: './ports.component.html',
  styleUrls: ['./ports.component.css']
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
