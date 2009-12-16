﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProcessHacker.Common;

namespace ProcessHacker.Native.Mfs
{
    public class MemoryDataStream : Stream
    {
        private MemoryObject _obj;
        private byte[] _buffer = new byte[MemoryFileSystem.MfsDataCellDataMaxLength];
        private int _bufferLength;

        internal MemoryDataStream(MemoryObject obj)
        {
            _obj = obj;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanTimeout
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return 0; }
            set { }
        }

        protected override void Dispose(bool disposing)
        {
            this.Flush();
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            if (_bufferLength > 0)
            {
                _obj.AppendData(_buffer, 0, _bufferLength);
                _bufferLength = 0;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int availableBufferLength = _buffer.Length - _bufferLength;

            Utils.ValidateBuffer(buffer, offset, count);

            if (availableBufferLength == 0)
            {
                this.Flush();
            }
            else
            {
                if (count < availableBufferLength)
                    availableBufferLength = count;

                Array.Copy(buffer, offset, _buffer, _bufferLength, availableBufferLength);
                _bufferLength += availableBufferLength;
                offset += availableBufferLength;
                count -= availableBufferLength;

                if (count > 0)
                    this.Flush();
            }

            if (count > 0)
            {
                _obj.AppendData(buffer, offset, count);
            }
        }

        public override void WriteByte(byte value)
        {
            if (_bufferLength >= _buffer.Length)
                this.Flush();

            _buffer[_bufferLength++] = value;
        }
    }
}
